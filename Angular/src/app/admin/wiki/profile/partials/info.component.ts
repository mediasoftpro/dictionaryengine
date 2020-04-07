/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

import { Component, Input } from "@angular/core";

@Component({
  selector: "app-wiki-info",
  templateUrl: "./info.html"
})
export class WikiProfileInfoComponent {
  constructor() {}

  @Input() Info: any = {};
  @Input() Author_FullName = "";
}
