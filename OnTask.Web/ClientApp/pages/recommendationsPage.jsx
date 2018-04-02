/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';
import BigCalendar from 'react-big-calendar';
import DatePicker from 'material-ui/DatePicker';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import MenuItem from 'material-ui/MenuItem';
import SelectField from 'material-ui/SelectField';
import moment from 'moment';
import toastr from 'toastr';

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';
import * as recommendationActions from 'ClientApp/actions/recommendationActions';
import CalendarDialogContent from 'ClientApp/components/calendar/calendarDialogContent';
import CalendarToolbar from 'ClientApp/components/calendar/calendarToolbar';
import RecommendationsToolbar from 'ClientApp/components/recommendations/recommendationsToolbar';

BigCalendar.setLocalizer(BigCalendar.momentLocalizer(moment))

class RecommendationsPage extends React.Component {
    constructor(props, context) {
        super(props, context);
        
        this.state = {
            isRecommendationDialogOpen: false,
            isRecalculateDialogOpen: false,
            selectedRecommendation: {
                event: {}
            },
            recalculating: false,
            recalculatedEnd: moment(new Date()).add(1, 'M').toDate(),
            recalculatedMode: Constants.DEFAULT_RECALCULATION_MODE
        };
    }

    handleSelectRecommendation = (recommendation, e) => {
        this.setState({ selectedRecommendation: recommendation });
        this.handleRecommendationDialogOpen();
    }

    handleRecommendationDialogOpen = () => {
        this.setState({ isRecommendationDialogOpen: true });
    }

    handleRecommendationDialogClose = () => {
        this.setState({ isRecommendationDialogOpen: false });
    }

    handleRecalculateClick = e => {
        this.handleRecalculateDialogOpen();
    }

    handleRecalculateOkClick = e => {
        e.preventDefault();
        this.props.actions.getRecommendations(
            moment(this.state.recalculatedEnd).format(Constants.MOMENT_DATE_FORMAT),
            this.state.recalculatedMode)
            .then(() => toastr.success('Recalculated successfully.'))
            .catch(error => {
                toastr.error(error);
            });
        this.setState({
            recalculatedEnd: moment(new Date()).add(1, 'M').toDate(),
            recalculatedMode: Constants.DEFAULT_RECALCULATION_MODE
        });
        this.handleRecalculateDialogClose();
    }

    handleRecalculateDialogOpen = () => {
        this.setState({ isRecalculateDialogOpen: true });
    }

    handleRecalculateDialogClose = () => {
        this.setState({ isRecalculateDialogOpen: false });
    }

    handleRecalculatedEndChange = (name, value) => {
        return this.setState({ recalculatedEnd: value });
    }

    handleRecalculatedModeChange = (e, index, value) => {
        return this.setState({ recalculatedMode: value });
    }

    canRecalculate = () => {
        return this.state.recalculatedEnd === null ||
            this.state.recalculatedMode === null;
    }

    render() {
        const views = ['month'];
        const { recommendations } = this.props;
        const {
            isRecommendationDialogOpen,
            isRecalculateDialogOpen,
            selectedRecommendation,
            recalculating,
            recalculatedEnd,
            recalculatedMode
        } = this.state;
        const recommendationActions = [
            <FlatButton
                label="Close"
                onClick={this.handleRecommendationDialogClose}
            />
        ];
        const recalculateActions = [
            <FlatButton
                label="Cancel"
                onClick={this.handleRecalculateDialogClose}
            />,
            <FlatButton
                label="OK"
                disabled={this.canRecalculate()}
                onClick={this.handleRecalculateOkClick}
            />
        ];
        
        return (
            <div className="calendar-container">
                <BigCalendar
                    events={recommendations}
                    components={{
                        toolbar: CalendarToolbar
                    }}
                    views={views}
                    step={30}
                    popup={true}
                    selectable={true}
                    showMultiDayTimes={true}
                    toolbar={true}
                    defaultDate={new Date()}
                    titleAccessor="name"
                    startAccessor="recommendedStartDate"
                    endAccessor="recommendedEndDate"
                    allDayAccessor="isAllDay"
                    onSelectEvent={this.handleSelectRecommendation}
                />
                <RecommendationsToolbar handleClick={this.handleRecalculateClick} />
                <Dialog
                    actions={recommendationActions}
                    modal={false}
                    open={isRecommendationDialogOpen}
                    onRequestClose={this.handleRecommendationDialogClose}>
                    <CalendarDialogContent
                        name={selectedRecommendation.event.name}
                        description={selectedRecommendation.event.description}
                        eventParentName={selectedRecommendation.event.eventParentName}
                        eventGroupName={selectedRecommendation.event.eventGroupName}
                        eventTypeName={selectedRecommendation.event.eventTypeName}
                        isAllDay={selectedRecommendation.event.isAllDay}
                        startDateTime={selectedRecommendation.event.startDateTime}
                        endDateTime={selectedRecommendation.event.endDateTime}
                        weight={selectedRecommendation.event.weight}
                    />
                </Dialog>
                <Dialog
                    title="Recalculate Recommendations"
                    actions={recalculateActions}
                    modal={false}
                    open={isRecalculateDialogOpen}
                    onRequestClose={this.handleRecalculateDialogClose}>
                    <DatePicker
                        name="recalculateEnd"
                        floatingLabelText="Date range end"
                        disabled={recalculating}
                        value={recalculatedEnd}
                        onChange={this.handleRecalculatedEndChange}
                        firstDayOfWeek={0}
                    />
                    <SelectField
                        name="recalculateMode"
                        floatingLabelText="Mode"
                        disabled={recalculating}
                        value={recalculatedMode}
                        onChange={this.handleRecalculatedModeChange}>
                        {Constants.RECALCULATION_MODES.map(mode =>
                            <MenuItem key={mode.value} value={mode.value} primaryText={mode.name} />
                        )}
                    </SelectField>
                </Dialog>
            </div>
        );
    }
}

RecommendationsPage.propTypes = {
    actions: PropTypes.object.isRequired,
    recommendations: PropTypes.array,
    routerActions: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
    return {
        recommendations: state.recommendations
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(recommendationActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(RecommendationsPage);
