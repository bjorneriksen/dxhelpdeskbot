import * as ko from "knockout";

class ModalBindingHandler {

    init(element, valueAccessor) {
        var content = valueAccessor();
        if( content.shown ) {
            $(element).on("shown.bs.modal", () => {
                content.shown();
            });
        }
    }

    update(element, valueAccessor) {
    }
}

ko.bindingHandlers.modal = new ModalBindingHandler();