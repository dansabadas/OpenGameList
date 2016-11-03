import {Component} from "@angular/core";

@Component({
    selector: "home",
    template: `
        <h2>{{title}}</h2>
        <item-list cls="latest"></item-list>
        <item-list cls="most-viewed"></item-list>
        <item-list cls="random"></item-list>
    `,
    styles: []
})

export class HomeComponent {
    title = "Welcome View";
}
