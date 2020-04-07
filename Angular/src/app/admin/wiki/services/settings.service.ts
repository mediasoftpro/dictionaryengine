/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Injectable } from "@angular/core";
import * as OPTIONS from "../wiki.model";
import { iUploadOptions } from "../../core/core.model";
import { AppConfig } from "../../../configs/app.config";
import { CoreService } from "../../core/coreService";
import { ButtonCSS, ICONCSS, ThemeCSS } from "../../../configs/themeSettings";

@Injectable()
export class SettingsService {
  // configurations
  private apiOptions: OPTIONS.IAPIOptions;

  private uploadOptions: iUploadOptions;
  private toolbarOptions: any;
  private searchOptions: any;

  constructor(private coreService: CoreService, public config: AppConfig) {
    const APIURL = config.getConfig("host");
    this.apiOptions = {
      load: APIURL + "api/wiki/load",
      getinfo: APIURL + "api/wiki/getinfo",
      action: APIURL + "api/wiki/action",
      proc: APIURL + "api/wiki/proc"
    };

    this.init_toolbar_options();
    this.init_search_options();
  }

  init_search_options() {
    this.searchOptions = {
      showpanel: true, // show, hide whole panel
      showSearchPanel: true,
      showAdvanceSearchLink: false,
      term: "",
      topselectioncheck: true,
      navList: [],
      filters: [],
      dropdownFilters: [],
      checkFilters: [],
      categories: [],
      selectedcategory: "",
      singleaction: false,
      actions: this.navActions()
    };
  }

  navActions() {
    return [
      {
        id: 1,
        title: "Create Wiki",
        tooltip: "Create new Wiki",
        row: 1,
        icon: "icon-file-plus",
        options: {},
        css: "btn m-b-5 btn-block btn-success",
        event: "add"
      }
    ];
  }

  init_search_profile_options() {
    return {
      showpanel: true, // show, hide whole panel
      showSearchPanel: false,
      term: "",
      topselectioncheck: false,
      navList: this.navList(),
      filters: [],
      dropdownFilters: [],
      categories: [],
      selectedcategory: "",
      singleaction: false,
      actions: this.navActions()
    };
  }
  navList() {
    return [
      {
        id: 1,
        title: "Edit",
        clickevent: true,
        event: "edit",
        css: "",
        url: "",
        icon: ""
      },
      {
        id: 2,
        title: "Delete",
        clickevent: true,
        event: "delete",
        css: "",
        url: "",
        icon: ""
      }
    ];
  }
  getSearchOptionsProfile() {
    return this.init_search_profile_options();
  }
  init_toolbar_options() {
    this.toolbarOptions = {
      showtoolbar: true,
      showsecondarytoolbar: true,
      showcheckAll: false,
      navbarcss: ThemeCSS.NAVBAR_CSS,
      left_options: [],
      left_caption: "",
      right_caption: "",
      right_options: [],
      actions: []
    };

    this.toolbarOptions.right_options.push({
      title: "Order",
      ismultiple: true,
      position: "right",
      icon: "icon-sort-by-order-alt position-left",
      Options: [
        {
          id: "0",
          title: "Date",
          value: "id",
          isclick: true,
          clickevent: "orderby",
          tooltip: "Order by Date"
        },
        {
          id: "1",
          title: "Term",
          value: "term",
          isclick: true,
          clickevent: "orderby",
          tooltip: "Order by Term"
        }
      ]
    });

    this.toolbarOptions.actions.push({
      title: "Mark As",
      ismultiple: true,
      icon: "",
      Options: [
        {
          id: "2",
          title: "Delete",
          value: 0,
          actionstatus: "delete",
          css: ButtonCSS.DANGER_BUTTON,
          attr: "",
          isclick: true,
          clickevent: "m_markas",
          icon: ICONCSS.DELETE_ICON,
          tooltip: "Delete selected records"
        }
      ]
    });
  }

  getApiOptions() {
    return this.apiOptions;
  }

  getUploadOptions() {
    return this.uploadOptions;
  }

  getToolbarOptions() {
    return this.toolbarOptions;
  }

  getSearchOptions() {
    return this.searchOptions;
  }
  getInitObject(): OPTIONS.WikiEntity {
    return {
      id: 0,
      term: "",
      term_complete: "",
      description: "",
      tags: "",
      replyid: 0,
      userid: 0
    };
  }
}
