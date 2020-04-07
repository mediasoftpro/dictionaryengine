/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Injectable } from "@angular/core";
import { dispatch } from "@angular-redux/store";
import { FluxStandardAction } from "flux-standard-action";
import { tassign } from "tassign";

import { IWikiState } from "./model";
type Payload = any;
interface MetaData {}
export type WikiAPIAction = FluxStandardAction<Payload, MetaData>;

@Injectable()
export class WikiAPIActions {
  static readonly LOAD_STARTED = "WIKI_LOAD_STARTED";
  static readonly LOAD_SUCCEEDED = "WIKI_LOAD_SUCCEEDED";
  static readonly LOAD_FAILED = "WIKI_LOAD_FAILED";

  static readonly APPLY_CHANGES = "WIKI_APPLY_CHANGES";
  static readonly APPLY_CHANGES_SUCCEEDED = "WIKI_APPLY_CHANGES_SUCCEEDED";
  static readonly APPLY_CHANGES_FAILED = "WIKI_APPLY_CHANGES_SUCCEEDED";

  static readonly UPDATE_FILTER_OPTIONS = "WIKI_UPDATE_FILTER_OPTIONS";
  static readonly APPLY_FILTER = "WIKI_APPLY_FILTER";
  static readonly UPDATE_PAGINATION_CURRENTPAGE =
    "WIKI_UPDATE_PAGINATION_CURRENTPAGE";
  static readonly UPDATE_CATEGORIES = "WIKI_UPDATE_CATEGORIES";

  static readonly SELECT_ALL = "WIKI_SELECT_ALL";
  static readonly IS_ITEM_SELECTED = "WIKI_IP_IS_ITEM_SELECTED";

  static readonly ADD_RECORD = "WIKI_ADD_RECORD";
  static readonly UPDATE_RECORD = "WIKI_UPDATE_RECORD";
  static readonly REMOVE_RECORD = "WIKI_REMOVE_RECORD";

  // REFERESH LOAD
  static readonly LOAD_END = "USERS_YT_LOADEND";
  static readonly REFRESH_DATA = "USERS_REFRESH_DATA";
  static readonly REFRESH_PAGINATION = "USERS_REFRESH_PAGINATION";

  @dispatch()
  loadStarted = (): WikiAPIAction => ({
    type: WikiAPIActions.LOAD_STARTED,
    //  meta: { },
    payload: null
  });

  @dispatch()
  loadSucceeded = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.LOAD_SUCCEEDED,
    //  meta: { },
    payload
  });

  @dispatch()
  loadFailed = (error): WikiAPIAction => ({
    type: WikiAPIActions.LOAD_FAILED,
    //  meta: { },
    payload: null,
    error
  });

  @dispatch()
  applyChanges = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.APPLY_CHANGES,
    //  meta: { },
    payload
  });

  @dispatch()
  actionSucceeded = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.APPLY_CHANGES_SUCCEEDED,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  actionFailed = (error): WikiAPIAction => ({
    type: WikiAPIActions.APPLY_CHANGES_SUCCEEDED,
    //  meta: { },
    payload: null,
    error
  });

  @dispatch()
  updateFilterOptions = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.UPDATE_FILTER_OPTIONS,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  applyFilter = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.APPLY_FILTER,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  updatePaginationCurrentPage = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.UPDATE_PAGINATION_CURRENTPAGE,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  updateCategories = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.UPDATE_CATEGORIES,
    //  meta: { },
    payload: payload
  });
  @dispatch()
  selectAll = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.SELECT_ALL,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  updateItemsSelectionStatus = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.IS_ITEM_SELECTED,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  addRecord = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.ADD_RECORD,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  updateRecord = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.UPDATE_RECORD,
    //  meta: { },
    payload: payload
  });

  @dispatch()
  loadEnd = (): WikiAPIAction => ({
    type: WikiAPIActions.LOAD_END,
    //  meta: { },
    payload: null
  });

  @dispatch()
  reloadList = (): WikiAPIAction => ({
    type: WikiAPIActions.REFRESH_DATA,
    //  meta: { },
    payload: null
  });

  @dispatch()
  refresh_pagination = (payload: Payload): WikiAPIAction => ({
    type: WikiAPIActions.REFRESH_PAGINATION,
    //  meta: { },
    payload: payload
  });
}

export class WikiBLL {
  loadSucceeded(state: IWikiState, action: any) {
    // update totalrecords object in pagination prop
    const _pagination = state.pagination;
    _pagination.totalRecords = action.payload.records;
    _pagination.pageSize = state.filteroptions.pagesize;
    _pagination.currentPage = state.filteroptions.pagenumber;
    // avoid loading categories again in next call
    const _filteroption = state.filteroptions;
    _filteroption.loadstats = false;

    return tassign(state, {
      posts: action.payload.posts,
      records: action.payload.records,
      pagination: _pagination,
      filteroptions: _filteroption,
      loading: false,
      isloaded: true
    });
  }

  applyFilterChanges(state: IWikiState, action: any) {
    const filters = state.filteroptions;
    for (const prop in filters) {
      if (prop === action.payload.attr) {
        filters[prop] = action.payload.value;
      }
    }
    filters.track_filter = true; // force filter subscriber to call loadRecord function to refresh data
    return tassign(state, {
      filteroptions: Object.assign({}, state.filteroptions, filters)
    });
  }

  updatePagination(state: IWikiState, action: any) {
    const pagination = state.pagination;
    pagination.currentPage = action.payload.currentpage;

    return tassign(state, {
      pagination: Object.assign({}, state.pagination, pagination)
    });
  }

  selectAll(state: IWikiState, action: any) {
    const posts = state.posts;
    for (const item of posts) {
      item.Selected = action.payload;
    }

    return tassign(state, {
      selectall: action.payload,
      posts: posts,
      itemsselected: action.payload
    });
  }

  addRecord(state: IWikiState, action: any) {
    const posts = state.posts;
    posts.unshift(action.payload);
    return tassign(state, { posts: posts });
  }

  updateRecord(state: IWikiState, action: any) {
    const posts = state.posts;
    for (let post of posts) {
      if (post.id === action.payload.id) {
        post = Object.assign({}, post, action.payload);
      }
    }
    return tassign(state, { posts: Object.assign([], state.posts, posts) });
  }

  /*  removeRecord(state: IWikiState, action: any) {
      const posts = state.posts;
      console.log('remove record');
      console.log(action.payload);

      if (action.payload.index > -1) {
         posts.splice(action.payload.index, 1);
      }
      return tassign(state, { posts: Object.assign([], state.posts, posts) });
  } */

  applyChanges(state: IWikiState, action: any) {
    const _updated_state = state.posts;
    for (const selected of action.payload.SelectedItems) {
      for (const item of _updated_state) {
        if (item.id === selected.id) {
          if (selected.actionstatus === "delete") {
            item.isdeleted = true;
          } else {
            for (const prop in item) {
              if (prop === action.payload.isenabled.attr) {
                item[prop] = action.payload.isenabled.value;
              }
            }
          }
        }
      }
    }
    return tassign(state, { posts: _updated_state });
  }

  refreshpagination(state: IWikiState, action: any) {
    const pagination = state.pagination;
    pagination.totalRecords = action.payload.totalrecords;
    pagination.pageSize = action.payload.pagesize;
    return tassign(state, { pagination: pagination });
    // return tassign(state, { pagination: Object.assign({}, state.pagination, pagination) });
  }
}
