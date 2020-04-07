/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Injectable } from "@angular/core";
import * as Controls from "../../../partials/components/dynamicform/model/elements";
import { FormBase } from "../../../partials/components/dynamicform/model/base";
import * as OPTIONS from "../wiki.model";
import { CoreService } from "../../../admin/core/coreService";

@Injectable()
export class FormService {

  constructor(private coreService: CoreService) {}

  getControls(entity: OPTIONS.WikiEntity) {
    const controls: FormBase<any>[] = [];
    if (entity.replyid === 0) {
      controls.push(
        new Controls.Textbox({
          key: "term",
          label: "Term",
          value: entity.term,
          required: true,
          order: 0,
          maxLength: 300,
          helpblock: `Enter term in short words`
        })
      );

      controls.push(
        new Controls.Textbox({
          key: "term_complete",
          label: "Term (Complete)",
          value: entity.term_complete,
          required: true,
          order: 1,
          maxLength: 1000,
          helpblock: `Enter complete term / topic`
        })
      );
    }

    controls.push(
      new Controls.TinyMyceEditor({
        key: "description",
        label: "Description",
        value: entity.description,
        required: true,
        tinymiceOptions: this.coreService.prepareInitAdvacneEditorSettings(),
        order: 2
      })
    );

    if (entity.replyid === 0) {
      controls.push(
        new Controls.Textbox({
          key: "tags",
          label: "Tags (Optional)",
          value: entity.tags,
          order: 3,
          maxLength: 1000,
          helpblock: `Enter one or more labels / tags separated by comma`
        })
      );
    }

    return controls.sort((a, b) => a.order - b.order);
  }
}
