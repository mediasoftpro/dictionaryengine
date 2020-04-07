
/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { select, select$ } from "@angular-redux/store";
import { Observable } from "rxjs/Observable";

// services
import { SettingsService } from "../services/settings.service";
import { DataService } from "../services/data.service";
import { FormService } from "../services/form.service";

// shared services
import { CoreService } from "../../core/coreService";
import { CoreAPIActions } from "../../../reducers/core/actions";

// reducer actions
import { WikiAPIActions } from "../../../reducers/wiki/actions";
import { fadeInAnimation } from "../../../animations/core";

import { PermissionService } from "../../../admin/users/services/permission.service";

@Component({
  templateUrl: "./process.html",
  encapsulation: ViewEncapsulation.None,
  animations: [fadeInAnimation],
  host: { "[@fadeInAnimation]": "" }
})
export class WikiProfileComponent implements OnInit {
  constructor(
    private settingService: SettingsService,
    private dataService: DataService,
    private coreService: CoreService,
    private coreActions: CoreAPIActions,
    private actions: WikiAPIActions,
    private route: ActivatedRoute,
    private formService: FormService,
    private permission: PermissionService,
    private router: Router
  ) {}

  ToolbarOptions: any;
  ToolbarLogOptions: any;
  isItemsSelected = true;
  RecordID = 0;
  SearchOptions: any;
  FilterOptions: any = {};
  controls: any = [];
  showLoader = false;
  formHeading = "Wiki Detail";
  submitText = "Submit";
  Info: any = {};
  uploadedFiles = [];
  SelectedItems: any = [];
  Author_FullName = "";

  @select(["users", "auth"])
  readonly auth$: Observable<any>;

  // permission logic
  isAccessGranted = false; // Granc access on resource that can be full access or read only access with no action rights
  isActionGranded = false; // Grand action on resources like add / edit /delete
  ngOnInit() {
    // user authentication & access right management
    // full resource access key and readonly key can be generated via roles management
    this.auth$.subscribe((auth: any) => {
      const FullAccessID = "1521396235692";
      const ReadOnlyAccessID = "1521396214790";
      if (
        this.permission.GrandResourceAccess(
          false,
          FullAccessID,
          ReadOnlyAccessID,
          auth.Role
        )
      ) {
        this.isAccessGranted = true;
        if (this.permission.GrandResourceAction(FullAccessID, auth.Role)) {
          this.isActionGranded = true;
        }
      }
    });

    // this.SearchOptions = this.settingService.getSearchOptionsProfile();
    this.ToolbarOptions = this.settingService.getToolbarOptions();
    this.ToolbarOptions.showtoolbar = false;
    this.ToolbarOptions.showcheckAll = false;
    this.ToolbarOptions.showsecondarytoolbar = true;
    this.ToolbarOptions.ProfileView = true;

    // fetch param from url
    this.route.params.subscribe(params => {
      this.RecordID = Number.parseInt(params["id"], 10);
      if (isNaN(this.RecordID)) {
        this.RecordID = 0;
      }
      if (this.RecordID > 0) {
        this.LoadInfo();
      }
    });
  }

  LoadInfo() {
    this.showLoader = true;
    this.dataService.GetDetailInfo(this.RecordID).subscribe((data: any) => {
      if (data.status === "error") {
        this.coreActions.Notify({
          title: data.message,
          text: "",
          css: "bg-success"
        });
        // redirect to main page
        this.router.navigate(["/wiki"]);
      } else {
        this.Info = data.post;
        this.SelectedItems = []; // reset
        for (const item of this.Info) {
          // tag processing
          if (item.updated_at !== null) {
            item.updated_at = new Date(parseInt(item.updated_at.substr(6), 10));
          }
          if (item.tags !== null && item.tags !== "") {
            item.tags_arr = this.ProcessCategories(item.tags.split(","));
          } else {
            item.tags_arr = [];
          }
          this.SelectedItems.push(item);
        }
      }
      this.showLoader = false;
    });
  }
  ProcessCategories(categories) {
    const arr = [];
    for (const category of categories) {
      arr.push({
        title: category.trim(),
        slug: category
          .trim()
          .replace(/\s+/g, "-")
          .toLowerCase()
      });
    }

    return arr;
  }
  toolbaraction(selection: any) {
    switch (selection.action) {
      case "m_markas":
        this.ProcessActions(selection.value);
        break;
    }
  }

  ProcessActions(selection: any) {
    if (!this.isActionGranded) {
      this.coreActions.Notify({
        title: "Permission Denied",
        text: "",
        css: "bg-danger"
      });
      return;
    }
    if (this.SelectedItems.length > 0) {
      for (const item of this.SelectedItems) {
        item.actionstatus = selection.actionstatus;
      }
      this.dataService.ProcessActions(this.SelectedItems, selection);
      if (selection.actionstatus === "delete") {
        this.router.navigate(["/wiki/"]);
      }
    }
  }
}
