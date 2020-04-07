/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { select } from "@angular-redux/store";
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
  animations: [fadeInAnimation]
})
export class WikiProcessComponent implements OnInit {
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

  @select(["users", "auth"])
  readonly auth$: Observable<any>;

  // permission logic
  isAccessGranted = false; // Granc access on resource that can be full access or read only access with no action rights
  isActionGranded = false; // Grand action on resources like add / edit /delete

  RecordID = 0;
  ReplyID = 0;
  SearchOptions: any;
  controls: any = [];
  showLoader = false;
  formHeading = "Add Wiki Topic";
  submitText = "Create Topic";
  Info: any;
  Auth: any = {};

  ngOnInit() {
    // user authentication & access right management
    // full resource access key and readonly key can be generated via roles management
    this.auth$.subscribe((auth: any) => {
      this.Auth = auth;
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

    // fetch param from url
    this.route.params.subscribe(params => {
      this.RecordID = Number.parseInt(params["id"], 10);
      this.ReplyID = Number.parseInt(params["replyid"], 10);

      if (isNaN(this.RecordID)) {
        this.RecordID = 0;
      }
      if (isNaN(this.ReplyID)) {
        this.ReplyID = 0;
      }

      if (this.RecordID > 0) {
        this.formHeading = "Update Wiki Topic";
        this.submitText = "Update";
        this.LoadInfo();
      } else if (this.ReplyID > 0) {
        this.formHeading = "Post Suggestion";
        this.submitText = "Submit";
        this.LoadInfo();
      } else {
        // initialize controls with default values
        this.initializeControls(this.settingService.getInitObject());
      }
    });
  }

  LoadInfo() {
    this.showLoader = true;
    console.log("rrccc " + this.ReplyID);
    this.dataService.GetInfo(this.RecordID).subscribe((data: any) => {
      if (data.status === "success") {
        if (this.ReplyID > 0) {
          // new post with reply
          this.Info = data.post;
          const _obj = this.settingService.getInitObject();
          _obj.replyid = this.ReplyID;
          this.initializeControls(_obj);
        } else {
          // update post
          this.initializeControls(data.post);
        }
      } else {
        this.coreActions.Notify({
          title: data.message,
          text: "",
          css: "bg-error"
        });
        this.initializeControls(this.settingService.getInitObject());
      }
      this.showLoader = false;
    });
  }

  initializeControls(data: any) {
    this.controls = this.formService.getControls(data);
  }

  SubmitForm(payload) {
    if (!this.isActionGranded) {
      this.coreActions.Notify({
        title: "Permission Denied",
        text: "",
        css: "bg-danger"
      });
      return;
    }
    this.showLoader = true;
    let _status = "Added";
    if (this.RecordID > 0) {
      payload.id = this.RecordID;
      _status = "Updated";
    }

    payload.userid = this.Auth.User.id;
    payload.replyid = this.ReplyID;
    if (this.ReplyID > 0) {
      payload.term = this.Info.term;
      payload.term_complete = this.Info.term_complete;
      payload.tags = this.Info.tags;
    }
    this.dataService.AddRecord(payload).subscribe(
      (data: any) => {
        if (data.status === "error") {
          this.coreActions.Notify({
            title: data.message,
            text: "",
            css: "bg-error"
          });
        } else {
          this.coreActions.Notify({
            title: "Record " + _status + " Successfully",
            text: "",
            css: "bg-success"
          });

          // enable reload action to refresh data
          this.actions.reloadList();

          // redirect
          if (this.ReplyID > 0) {
            this.router.navigate(["/wiki/profile/" + this.ReplyID]);
          } else {
            this.router.navigate(["/wiki/profile/" + data.record.id]);
          }
        }
        this.showLoader = false;
      },
      err => {
        this.showLoader = false;
        this.coreActions.Notify({
          title: "Error Occured",
          text: "",
          css: "bg-danger"
        });
      }
    );
  }
}
