export function checkBlankEventParent(eventParent) {
    let newEventParent = Object.assign({}, eventParent);
    newEventParent.description = checkBlankReturnNull(eventParent.description);
    newEventParent.weight = checkBlankReturnNull(eventParent.weight);
    return newEventParent;
}

export function checkNullEventParent(eventParent) {
    let newEventParent = Object.assign({}, eventParent);
    newEventParent.description = checkNullReturnBlank(eventParent.description);
    newEventParent.weight = checkNullReturnBlank(eventParent.weight);
    return newEventParent;
}

function checkBlankReturnNull(item) {
    return item === '' ? null : item;
}

function checkNullReturnBlank(item) {
    return item === null ? '' : item;
}
