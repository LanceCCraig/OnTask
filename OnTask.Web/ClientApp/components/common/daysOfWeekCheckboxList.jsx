/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import Checkbox from 'material-ui/Checkbox';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import TextField from 'material-ui/TextField';

/**
 * Internal dependencies
 */
import Constants from 'ClientApp/constants';

class DaysOfWeekCheckboxList extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            isDialogOpen: false,
            temporaryValues: props.values,
            values: props.values
        };
    }

    getOrderedDays = selectedDays => {
        let orderedDays = [];
        for (let i = 0; i < Constants.DAYS_OF_WEEK.length; i++) {
            if (selectedDays.indexOf(Constants.DAYS_OF_WEEK[i]) !== -1) {
                orderedDays.push(Constants.DAYS_OF_WEEK[i]);
            }
        }
        return orderedDays;
    }

    handleCheck = day => {
        let temporaryValues = [...this.state.temporaryValues];
        let newTemporaryValues = [];
        if (temporaryValues.includes(day)) {
            newTemporaryValues = temporaryValues.filter(temporaryValue => temporaryValue !== day);
        } else {
            newTemporaryValues = [...temporaryValues, day];
        }
        return this.setState({ temporaryValues: this.getOrderedDays(newTemporaryValues) });
    }

    handleDialogClose = () => {
        this.setState({ isDialogOpen: false });
    }

    handleDialogOpen = () => {
        this.setState({ isDialogOpen: true });
    }

    handleDialogCancelClick = e => {
        let values = [...this.state.values];
        this.setState({ temporaryValues: values });
        this.handleDialogClose();
    }

    handleDialogOkClick = e => {
        let newValues = [...this.state.temporaryValues];
        this.setState({ values: newValues });
        this.props.onChange(newValues);
        this.handleDialogClose();
    }

    handleTextClick = e => {
        if (!this.props.disabled) {
            this.handleDialogOpen();
        }
    }

    render() {
        const actions = [
            <FlatButton
                label="Cancel"
                onClick={this.handleDialogCancelClick}
            />,
            <FlatButton
                primary={true}
                label="OK"
                onClick={this.handleDialogOkClick}
            />
        ];
        const { temporaryValues, values } = this.state;
        return (
            <div>
                <TextField
                    name="daysOfWeek"
                    floatingLabelText="Day(s) of week"
                    onClick={this.handleTextClick}
                    value={values.map(day => day.substr(0, 3)).join()}
                />
                <Dialog
                    title="Day(s) of Week"
                    actions={actions}
                    modal={false}
                    open={this.state.isDialogOpen}
                    onRequestClose={this.handleDialogClose}>
                    <div>
                        {Constants.DAYS_OF_WEEK.map(day =>
                            <Checkbox
                                key={day}
                                label={day}
                                onCheck={() => this.handleCheck(day)}
                                checked={temporaryValues.includes(day)}
                            />
                        )}
                    </div>
                </Dialog>
            </div>
        );
    }
}

DaysOfWeekCheckboxList.propTypes = {
    disabled: PropTypes.bool,
    errorText: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    values: PropTypes.array
};

export default DaysOfWeekCheckboxList;
