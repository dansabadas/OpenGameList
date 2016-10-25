import {Component, Input, OnInit} from "@angular/core";
import {Item} from "./item";
import {ItemService} from "./item.service";

@Component({
    selector: "item-list",
    template: `
        <h2>{{title}}</h2>
        <ul class="items">
            <li *ngFor="let item of items" 
                [class.selected]="item === selectedItem"
                (click)="onSelect(item)">
                <span>{{item.Title}}</span>
            </li>
        </ul>
        <item-detail *ngIf="selectedItem" [item]="selectedItem"></item-detail>
    `,  // the [item] thing above can be declared like this because the DetailComponent at its turn declared inside a  @Input("item") alias for the property
    styles: [`
        ul.items li { 
            cursor: pointer;
        }
        ul.items li.selected { 
            background-color: #dddddd; 
        }
    `]
})

export class ItemListComponent implements OnInit {
    @Input("cls") class: string;
    title: string;
    selectedItem: Item;
    items: Item[];
    errorMessage: string;

    constructor(private itemService: ItemService) { }

    ngOnInit() {
        //this.itemService.getLatest().subscribe(
        //    items => this.items = items,
        //    error => this.errorMessage = error
        //);
        console.log(`ItemListComponent instantiated with the following type: ${this.class}`);
        let s: any;
        switch (this.class) {
            case "latest":
            default:
                this.title = "Latest Items";
                s = this.itemService.getLatest();
                break;
            case "most-viewed":
                this.title = "Most Viewed Items";
                s = this.itemService.getMostViewed();
                break;
            case "random":
                this.title = "Random Items";
                s = this.itemService.getRandom();
                break;
        }

        s.subscribe(
            items => this.items = items,
            error => this.errorMessage = error
        );
    }

    onSelect(item: Item) {
        this.selectedItem = item;
        console.log(`item with Id ${this.selectedItem.Id} has been selected.`);
    }
}
