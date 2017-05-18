import * as ko from "knockout";
import * as modal from "./modalBindingHandler";
//import "signalr/js";

export class index {
    constructor() {
    }

    shown() {
        BotChat.App({
            directLine: {
                token: "WfnLyhBjpIE.dAA.TABCADYASwBFAHQAeQBBAEMARwB5AEEARQAzAHQAWQBxAFkAZwBPAGsANgA.3Zb6Nu3P0gE.DkF6YRTkbpc.gOaZ9elxuLma36i8-3Trni8N4wWhV1dQqUuQUrYMNSw"
            },
            user: { id: 'botchattest' },
            bot: { id: 'yourbot' },
            resize: 'detect'
        }, document.getElementById("bot"));

    }
}