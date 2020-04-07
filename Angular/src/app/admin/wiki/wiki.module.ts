/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { Routes, RouterModule } from "@angular/router";

import { WikiComponent } from "./wiki.component";
import { ListComponent } from "./partials/list.component";

// process view
import { WikiProcessModule } from "./process/process.module";
import { WikiProcessComponent } from "./process/process.component";
// profile view
import { WikiProfileModule } from "./profile/process.module";
import { WikiProfileComponent } from "./profile/process.component";
/* services */
import { SettingsService } from "./services/settings.service";
import { DataService } from "./services/data.service";

/* actions */
import { WikiAPIActions } from "../../reducers/wiki/actions";

import { PartialModule } from "../../partials/shared.module";

const routes: Routes = [
  {
    path: "",
    data: {
      title: "Wiki Management",
      urls: [
        { title: "Dashboard", url: "/" },
        { title: "Wiki", url: "/wiki" },
        { title: "Management" }
      ]
    },
    component: WikiComponent
  },
  {
    path: "tag/:tagname",
    data: {
      title: "Wiki Management",
      urls: [
        { title: "Dashboard", url: "/" },
        { title: "Wiki", url: "/wiki" },
        { title: "Management" }
      ]
    },
    component: WikiComponent
  },
  {
    path: "profile/:id",
    data: {
      title: "Wiki Information",
      urls: [
        { title: "Dashboard", url: "/" },
        { title: "Wiki", url: "/wiki" },
        { title: "Wiki Information" }
      ]
    },
    component: WikiProfileComponent
  },
  {
    path: "process/:id",
    data: {
      title: "Process Wiki",
      urls: [
        { title: "Dashboard", url: "/" },
        { title: "Wiki", url: "/wiki" },
        { title: "Process" }
      ]
    },
    component: WikiProcessComponent
  },
  {
    path: "process/:id/:replyid",
    data: {
      title: "Process Wiki",
      urls: [
        { title: "Dashboard", url: "/" },
        { title: "Wiki", url: "/wiki" },
        { title: "Process" }
      ]
    },
    component: WikiProcessComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PartialModule,
    WikiProfileModule,
    WikiProcessModule,
    NgbModule,
    RouterModule.forChild(routes)
  ],
  declarations: [WikiComponent, ListComponent],
  entryComponents: [],
  exports: [WikiComponent],
  providers: [SettingsService, DataService, WikiAPIActions]
})
export class WikiModule {}
