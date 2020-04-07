/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */
/*
import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges
} from "@angular/core";
import { AppConfig } from "../../configs/app.config";

@Component({
  selector: "app-user-avator",
  templateUrl: "./avator.html"
})
export class AvatorComponent implements OnChanges {
  constructor(public config: AppConfig) {}

  @Input() uploadedFiles = [];
  @Input() Info: any = {};
  @Output() OnUploaded = new EventEmitter<any>();
  InitUploader = false;
  uploadoptions = {
    handlerpath: this.config.getConfig("host") + "api/user/uploads",
    pickfilecaption: "Change Photo",
    pickbuttoncss: "btn btn-dark btn-rounded btn-block btn-sm",
    uploadfilecaption: "Start Uploading",
    maxfilesize: "8mb",
    chunksize: "8mb",
    headers: {},
    extensiontitle: "Images Files",
    extensions: "jpg,jpeg,png",
    filepath: "",
    username: "",
    removehandler: "",
    maxallowedfiles: 1,
    showFileName: false, // show filename with media file
    showoriginalSize: false, // show media in original size
    avatormode: true, // if enabled it will allow single upload and replace existing photo with uploaded photo
    value: []
  };

  ngOnChanges() {
    this.uploadoptions.username = this.Info.userName;
    if (this.uploadoptions.username !== undefined) {
      this.InitUploader = true;
    }
  }
  filesUploaded(files: any) {
    this.uploadedFiles = []; // need to replace existing image with uploaded image
    for (const file of files) {
      this.uploadedFiles.push(file);
    }
    this.OnUploaded.emit(this.uploadedFiles);
  }
}
*/