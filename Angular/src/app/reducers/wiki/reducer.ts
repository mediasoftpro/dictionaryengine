/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { WikiAPIAction, WikiAPIActions, WikiBLL } from "./actions";
import { IWikiState, WIKI_INITIAL_STATE } from "./model";
import { tassign } from "tassign";
import { Action } from "redux";

export function createWikiReducer() {
  return function wikiReducer(
    state: IWikiState = WIKI_INITIAL_STATE,
    a: Action
  ): IWikiState {
    const action = a as WikiAPIAction;

    const bll = new WikiBLL();
    /*if (!action.meta) {
      return state;
    }*/
    switch (action.type) {
      case WikiAPIActions.IS_ITEM_SELECTED:
        return tassign(state, { itemsselected: action.payload });

      case WikiAPIActions.SELECT_ALL:
        return bll.selectAll(state, action);

      case WikiAPIActions.LOAD_STARTED:
        return tassign(state, { loading: true, error: null });

      case WikiAPIActions.LOAD_SUCCEEDED:
        return bll.loadSucceeded(state, action);

      case WikiAPIActions.LOAD_FAILED:
        return tassign(state, { loading: false, error: action.error });

      /* update wholefilter options */
      case WikiAPIActions.UPDATE_FILTER_OPTIONS:
        return tassign(state, {
          filteroptions: Object.assign({}, state.filteroptions, action.payload)
        });

      /* update specific filter option */
      case WikiAPIActions.APPLY_FILTER:
        return bll.applyFilterChanges(state, action);

      /* update pagination current page */
      case WikiAPIActions.UPDATE_PAGINATION_CURRENTPAGE:
        return bll.updatePagination(state, action);

      /* add record */
      case WikiAPIActions.ADD_RECORD:
        return bll.addRecord(state, action);

      /* update record state */
      case WikiAPIActions.UPDATE_RECORD:
        return bll.updateRecord(state, action);

      /* apply changes (multiple selection items e.g delete selected records or enable selected records) */
      case WikiAPIActions.APPLY_CHANGES:
        return bll.applyChanges(state, action);
      // remove loader
      case WikiAPIActions.LOAD_END:
        return tassign(state, { loading: false });

      case WikiAPIActions.REFRESH_PAGINATION:
        return bll.refreshpagination(state, action);

      case WikiAPIActions.REFRESH_DATA:
        const filterOptions = state.filteroptions;
        filterOptions.track_filter = true;
        return tassign(state, {
          filteroptions: Object.assign({}, state.filteroptions, filterOptions)
        });
    }

    return state;
  };
}
