﻿///<reference path="../../typings/index.d.ts"/>
import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {HttpModule} from "@angular/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import "rxjs/Rx";

import {AboutComponent} from "./about.component";
import {AppComponent} from "./app.component";
import {HomeComponent} from "./home.component";
import {ItemListComponent} from "./item-list.component";
import {ItemDetailViewComponent} from "./item-detail-view.component";
import {ItemDetailEditComponent} from "./item-detail-edit.component";
import {LoginComponent} from "./login.component";
import {PageNotFoundComponent} from "./page-not-found.component";
import { AppRouting } from "./app.routing";
import { AuthHttp } from "./auth.http";
import {AuthService} from "./auth.service";
import { ItemService } from "./item.service";
import { UserEditComponent } from "./user-edit.component";

@NgModule({
    // directives, components, and pipes
    declarations: [
        AboutComponent,
        AppComponent,
        HomeComponent,
        ItemListComponent,
        ItemDetailViewComponent,
        ItemDetailEditComponent,
        LoginComponent,
        PageNotFoundComponent,
        UserEditComponent
    ],
    // modules
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRouting
    ],
    // providers
    providers: [
      AuthHttp,
      ItemService,
      AuthService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
