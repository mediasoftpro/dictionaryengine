(window.webpackJsonp=window.webpackJsonp||[]).push([[9],{My7I:function(t,e,i){"use strict";var n=i("fXoL"),o=i("1kSV"),c=i("ofXK");let s=(()=>{class t{constructor(t){this.Image="",this.CropOption=0,this.ShowZoomer=!0,this.OnCropped=new n.n,this.OnCroppedCancel=new n.n,this.el=t}ngOnInit(){}ngAfterViewInit(){if(""!==this.Image){if(1===this.CropOption){let t=350,e=350;void 0!==this.MediaSettings&&(void 0!==this.MediaSettings.width&&this.MediaSettings.width>0&&(t=this.MediaSettings.width),void 0!==this.MediaSettings.height&&this.MediaSettings.height>0&&(e=this.MediaSettings.height));let i=t+50,n=e+50;i<=200&&(i=400),n<=200&&(n=400),this.basic=$("#croppie-demo").croppie({enableExif:!0,enableResize:!1,enforceBoundary:!0,enableOrientation:!0,viewport:{width:t,height:e,type:"square"},boundary:{width:i,height:n}})}else if(0===this.CropOption){let t=400,e=400;void 0!==this.MediaSettings&&(void 0!==this.MediaSettings.user_thumbnail_width&&this.MediaSettings.user_thumbnail_width>0&&(t=this.MediaSettings.user_thumbnail_width),void 0!==this.MediaSettings.user_thumbnail_height&&this.MediaSettings.user_thumbnail_height>0&&(e=this.MediaSettings.user_thumbnail_height));let i=t+50,n=e+50;i<=200&&(i=400),n<=200&&(n=400),this.basic=$("#croppie-demo").croppie({enableExif:!0,enableResize:!1,enforceBoundary:!0,enableOrientation:!0,viewport:{width:t,height:e,type:"square"},boundary:{width:i,height:n}})}else 2===this.CropOption&&(this.basic=$("#croppie-demo").croppie({enableExif:!0,enableResize:!0,viewport:{width:1900,height:300,type:"square"},boundary:{width:2e3,height:400}}));this.basic.croppie("bind",{url:this.Image})}}left(){this.basic.croppie("bind",{url:this.Image,degrees:"-90"}).then((function(t){console.log("rotate left")}))}right(){this.basic.croppie("bind",{url:this.Image,degrees:"90"}).then((function(t){console.log("rotate right")}))}crop(){console.log("crop button clicked");const t=this;this.basic.croppie("result",{type:"canvas"}).then((function(e){t.OnCropped.emit({image:e})}))}cancel(){this.OnCroppedCancel.emit({})}}return t.\u0275fac=function(e){return new(e||t)(n.Mb(n.l))},t.\u0275cmp=n.Gb({type:t,selectors:[["app-croppie"]],inputs:{Image:"Image",MediaSettings:"MediaSettings",CropOption:"CropOption",ShowZoomer:"ShowZoomer"},outputs:{OnCropped:"OnCropped",OnCroppedCancel:"OnCroppedCancel"},decls:6,vars:0,consts:[[1,"text-center","m-t-10"],[1,"btn","btn-primary","m-r-5",3,"click"],[1,"btn","btn-default",3,"click"],["id","croppie-demo"]],template:function(t,e){1&t&&(n.Sb(0,"div",0),n.Sb(1,"button",1),n.ec("click",(function(){return e.crop()})),n.Gc(2,"Done"),n.Rb(),n.Sb(3,"button",2),n.ec("click",(function(){return e.cancel()})),n.Gc(4,"Cancel"),n.Rb(),n.Rb(),n.Nb(5,"div",3))},encapsulation:2}),t})();function a(t,e){if(1&t){const t=n.Tb();n.Sb(0,"div"),n.Sb(1,"div",5),n.Sb(2,"app-croppie",6),n.ec("OnCroppedCancel",(function(e){return n.xc(t),n.gc().close(e)}))("OnCropped",(function(e){return n.xc(t),n.gc().croppedImage(e)})),n.Rb(),n.Rb(),n.Rb()}if(2&t){const t=n.gc();n.zb(2),n.nc("CropOption",t.Info.cropoption)("Image",t.Info.data.original_picture)("MediaSettings",t.Info.settings)}}function r(t,e){if(1&t){const t=n.Tb();n.Sb(0,"div"),n.Sb(1,"app-croppie",6),n.ec("OnCroppedCancel",(function(e){return n.xc(t),n.gc().close(e)}))("OnCropped",(function(e){return n.xc(t),n.gc().croppedImage(e)})),n.Rb(),n.Rb()}if(2&t){const t=n.gc();n.zb(1),n.nc("CropOption",t.Info.cropoption)("Image",t.Info.data.original_picture)("MediaSettings",t.Info.settings)}}i.d(e,"a",(function(){return p}));let p=(()=>{class t{constructor(t){this.activeModal=t,this.CropOption=0}ngOnInit(){this.title=this.Info.title}croppedImage(t){this.activeModal.close({data:t}),console.log("close modal")}close(t){this.activeModal.dismiss("Cancel Clicked")}}return t.\u0275fac=function(e){return new(e||t)(n.Mb(o.a))},t.\u0275cmp=n.Gb({type:t,selectors:[["ng-component"]],inputs:{Info:"Info"},decls:8,vars:3,consts:[[1,"modal-header"],[1,"modal-title"],["type","button","aria-label","Close",1,"close",3,"click"],["aria-hidden","true"],[4,"ngIf"],["id","style-1",1,"scrollbar"],["id","croppie-demo",3,"CropOption","Image","MediaSettings","OnCroppedCancel","OnCropped"]],template:function(t,e){1&t&&(n.Sb(0,"div",0),n.Sb(1,"h4",1),n.Gc(2),n.Rb(),n.Sb(3,"button",2),n.ec("click",(function(){return e.activeModal.dismiss("Cross click")})),n.Sb(4,"span",3),n.Gc(5,"\xd7"),n.Rb(),n.Rb(),n.Rb(),n.Ec(6,a,3,3,"div",4),n.Ec(7,r,2,3,"div",4)),2&t&&(n.zb(2),n.Hc(e.title),n.zb(4),n.nc("ngIf",e.Info.scroller),n.zb(1),n.nc("ngIf",!e.Info.scroller))},directives:[c.n,s],encapsulation:2}),t})()},pBKm:function(t,e,i){"use strict";i.r(e);var n=i("ofXK"),o=i("3Pt+"),c=i("tyNb"),s=i("mrSG"),a=i("Usal"),r=i("pz8m"),p=i("7NWl"),d=i("zwrK"),b=i("5zJ1"),l=i("ReVU"),u=i("fXoL"),h=i("Hjmt"),g=i("1pjZ"),f=i("My7I"),m=i("1kSV");let S=(()=>{class t{constructor(t){this.modalService=t,this.OnCropped=new u.n,this.showUploadBtn=!0,this.CropOption=0,this.showLoader=!1}ngOnInit(){}save(){const t={id:this.Info.id,picturename:this.Info.cropped_picture};console.log(t),this.OnCropped.emit(t)}cancel(){this.showUploadBtn=!0,this.Info.cropped_picture=this.Info.original_picture}deleteImage(t){this.Info.cropped_picture="",this.Info.original_picture="",this.showLoader=!0,t.stopPropagation()}changeListener(t){this.readThis(t.target),this.showUploadBtn=!1}readThis(t){const e=t.files[0],i=new FileReader,n=this;i.onloadend=function(t){n.Info.original_picture=i.result.toString(),n.Info.cropped_picture=i.result.toString(),n.editThumbnail()},i.readAsDataURL(e)}editThumbnail(){const t=this.modalService.open(f.a,{backdrop:!1});t.componentInstance.Info={title:"Editor",data:this.Info,cropoption:this.CropOption,settings:this.MediaSettings,scroller:!1},t.result.then(t=>{this.Info.cropped_picture=t.data.image,this.Info.img_url=t.data.image,this.save()},t=>{console.log("dismissed")})}}return t.\u0275fac=function(e){return new(e||t)(u.Mb(m.b))},t.\u0275cmp=u.Gb({type:t,selectors:[["app-banneruploader"]],inputs:{Info:"Info",MediaSettings:"MediaSettings"},outputs:{OnCropped:"OnCropped"},decls:7,vars:1,consts:[[1,""],["alt","",1,"img-responsive","img-thumbnail",3,"src"],[1,"m-t-10"],["for","file-upload"],[1,"btn","btn-primary","text-center"],["id","file-upload","type","file",2,"display","none",3,"change"]],template:function(t,e){1&t&&(u.Sb(0,"div",0),u.Nb(1,"img",1),u.Sb(2,"div",2),u.Sb(3,"label",3),u.Sb(4,"span",4),u.Gc(5,"Change"),u.Rb(),u.Sb(6,"input",5),u.ec("change",(function(t){return e.changeListener(t)})),u.Rb(),u.Rb(),u.Rb(),u.Rb()),2&t&&(u.zb(1),u.oc("src",e.Info.img_url,u.zc))},encapsulation:2}),t})();function v(t,e){if(1&t){const t=u.Tb();u.Sb(0,"div",8),u.Sb(1,"app-banneruploader",9),u.ec("OnCropped",(function(e){return u.xc(t),u.gc().onImageUploaded(e)})),u.hc(2,"async"),u.Rb(),u.Rb()}if(2&t){const t=u.gc();u.zb(1),u.nc("Info",t.User)("MediaSettings",u.ic(2,2,t.configs$).general.media)}}function I(t,e){if(1&t&&(u.Sb(0,"h3",10),u.Gc(1),u.Rb()),2&t){const t=u.gc();u.zb(1),u.Jc(" ",t.User.firstname," ",t.User.lastname," ")}}function w(t,e){1&t&&(u.Sb(0,"h3",10),u.Gc(1," My Account "),u.Rb())}function O(t,e){if(1&t&&(u.Sb(0,"p",11),u.Gc(1),u.Rb()),2&t){const t=u.gc();u.zb(1),u.Ic(" Last Login: ",t.User.customize_last_login," ")}}function y(t,e){if(1&t&&(u.Sb(0,"p",11),u.Gc(1),u.Rb()),2&t){const t=u.gc();u.zb(1),u.Ic(" Register: ",t.User.customize_register_date," ")}}let R=(()=>{class t{constructor(t,e){this.config=t,this.dataService=e,this.View=new u.n,this.SelectedItems=new u.n,this.User={}}ngOnInit(){this.auth$.subscribe(t=>{this.User=t.User})}onImageUploaded(t){console.log("cropper hit"),this.dataService.UpdateAvator(t)}}return t.\u0275fac=function(e){return new(e||t)(u.Mb(g.a),u.Mb(p.a))},t.\u0275cmp=u.Gb({type:t,selectors:[["app-account-dashboard"]],outputs:{View:"View",SelectedItems:"SelectedItems"},features:[u.yb([p.a])],decls:12,vars:7,consts:[[1,"card"],[1,"card-body"],[1,"m-t-10","m-b-10"],[1,"row","m-b-10"],["class","col-md-4",4,"ngIf"],[1,"col-md-8"],["class","m-b-10",4,"ngIf"],["class","m-t-0 m-b-5",4,"ngIf"],[1,"col-md-4"],[3,"Info","MediaSettings","OnCropped"],[1,"m-b-10"],[1,"m-t-0","m-b-5"]],template:function(t,e){1&t&&(u.Sb(0,"div",0),u.Sb(1,"div",1),u.Sb(2,"div",2),u.Sb(3,"div",3),u.Ec(4,v,3,4,"div",4),u.hc(5,"async"),u.Sb(6,"div",5),u.Ec(7,I,2,2,"h3",6),u.Ec(8,w,2,0,"h3",6),u.Nb(9,"hr"),u.Ec(10,O,2,1,"p",7),u.Ec(11,y,2,1,"p",7),u.Rb(),u.Rb(),u.Rb(),u.Rb(),u.Rb()),2&t&&(u.zb(4),u.nc("ngIf",u.ic(5,5,e.configs$).general.media),u.zb(3),u.nc("ngIf",null!==e.User.firstname),u.zb(1),u.nc("ngIf",null===e.User.firstname||""===e.User.firstname),u.zb(2),u.nc("ngIf",e.User.last_login),u.zb(1),u.nc("ngIf",e.User.last_login))},directives:[n.n,S],pipes:[n.b],encapsulation:2}),Object(s.a)([Object(a.e)(["users","auth"])],t.prototype,"auth$",void 0),Object(s.a)([Object(a.e)(["configuration","configs"])],t.prototype,"configs$",void 0),t})(),C=(()=>{class t{constructor(t){this.config=t,this.User={},this.isAdmin=!0}ngOnInit(){this.auth$.subscribe(t=>{this.User=t.User})}}return t.\u0275fac=function(e){return new(e||t)(u.Mb(g.a))},t.\u0275cmp=u.Gb({type:t,selectors:[["app-admin-dashboard"]],decls:1,vars:0,consts:[[1,"row","m-t-10","m-t-20",2,"min-height","500px"]],template:function(t,e){1&t&&u.Nb(0,"div",0)},encapsulation:2}),Object(s.a)([Object(a.e)(["users","auth"])],t.prototype,"auth$",void 0),Object(s.a)([Object(a.e)(["configuration","configs"])],t.prototype,"configs$",void 0),t})(),M=(()=>{class t{}return t.\u0275fac=function(e){return new(e||t)},t.\u0275cmp=u.Gb({type:t,selectors:[["app-setup-dashboard"]],decls:10,vars:0,consts:[[1,"card","setup-container"],[1,"card-body"],[1,"m-b-20","text-center"],[1,"m-b-40","text-center"],["routerLink","/setup",1,"btn","btn-primary","btn-lg"]],template:function(t,e){1&t&&(u.Sb(0,"div",0),u.Sb(1,"div",1),u.Sb(2,"div",2),u.Sb(3,"h1"),u.Gc(4,"Setup Application"),u.Rb(),u.Rb(),u.Sb(5,"div",3),u.Gc(6," Click on start button to start setup process. Note: this page just for setup application, please remove this page from production version. "),u.Rb(),u.Sb(7,"div",2),u.Sb(8,"a",4),u.Gc(9,"Start"),u.Rb(),u.Rb(),u.Rb(),u.Rb())},directives:[c.e],encapsulation:2}),t})();function _(t,e){1&t&&u.Nb(0,"div")}let U=(()=>{class t{constructor(t){this.config=t}}return t.\u0275fac=function(e){return new(e||t)(u.Mb(g.a))},t.\u0275cmp=u.Gb({type:t,selectors:[["app-search"]],decls:2,vars:2,consts:[[3,"ngSwitch"],[4,"ngSwitchCase"]],template:function(t,e){1&t&&(u.Sb(0,"div",0),u.Ec(1,_,1,0,"div",1),u.Rb()),2&t&&(u.nc("ngSwitch",e.config.getGlobalVar("searchparams").type),u.zb(1),u.nc("ngSwitchCase","blogs"))},directives:[n.o,n.p],encapsulation:2}),t})();function G(t,e){1&t&&(u.Sb(0,"div"),u.Nb(1,"app-account-dashboard"),u.Rb())}function z(t,e){1&t&&(u.Sb(0,"div"),u.Nb(1,"app-admin-dashboard"),u.Rb())}function E(t,e){if(1&t&&(u.Sb(0,"div"),u.Ec(1,G,2,0,"div",0),u.Ec(2,z,2,0,"div",0),u.Rb()),2&t){const t=u.gc();u.zb(1),u.nc("ngIf","account"===t.config.getGlobalVar("apptype")),u.zb(1),u.nc("ngIf","admin"===t.config.getGlobalVar("apptype"))}}function j(t,e){1&t&&(u.Sb(0,"div"),u.Nb(1,"app-setup-dashboard"),u.Rb())}function N(t,e){1&t&&(u.Sb(0,"div"),u.Nb(1,"app-search"),u.Rb())}function V(t,e){1&t&&(u.Sb(0,"div"),u.Gc(1," Not Authenticated "),u.Rb())}function k(t,e){if(1&t&&(u.Sb(0,"div"),u.Ec(1,j,2,0,"div",0),u.Ec(2,N,2,0,"div",0),u.Ec(3,V,2,0,"div",0),u.Rb()),2&t){const t=u.gc();u.zb(1),u.nc("ngIf","setup"===t.config.getGlobalVar("apptype")),u.zb(1),u.nc("ngIf","search"===t.config.getGlobalVar("apptype")),u.zb(1),u.nc("ngIf","setup"!==t.config.getGlobalVar("apptype")&&"search"!==t.config.getGlobalVar("apptype"))}}let $=(()=>{class t{constructor(t,e,i){this.dataService=t,this.permission=e,this.config=i,this.showLoader=!1,this.isAccessGranted=!1,this.isActionGranded=!1,this.User={},this.uploadedFiles=[]}ngOnInit(){this.auth$.subscribe(t=>{this.User=t.User,""!==this.User.img_url&&([].push(),this.uploadedFiles.push({fname:this.User.img_url,filename:this.User.img_url,filetype:".jpg",url:this.User.img_url}))})}OnUploadedImages(t){this.dataService.UpdateThumb(this.User,t)}}return t.\u0275fac=function(e){return new(e||t)(u.Mb(p.a),u.Mb(h.a),u.Mb(g.a))},t.\u0275cmp=u.Gb({type:t,selectors:[["app-dashboard"]],features:[u.yb([p.a,d.a,b.a,r.a,l.a])],decls:4,vars:6,consts:[[4,"ngIf"]],template:function(t,e){1&t&&(u.Ec(0,E,3,2,"div",0),u.hc(1,"async"),u.Ec(2,k,4,3,"div",0),u.hc(3,"async")),2&t&&(u.nc("ngIf",u.ic(1,2,e.auth$).isAuthenticated),u.zb(2),u.nc("ngIf",!u.ic(3,4,e.auth$).isAuthenticated))},directives:[n.n,R,C,M,U],pipes:[n.b],encapsulation:2}),Object(s.a)([Object(a.e)(["users","auth"])],t.prototype,"auth$",void 0),Object(s.a)([Object(a.e)(["configuration","configs"])],t.prototype,"configs$",void 0),t})();var x=i("o+qO");let A=(()=>{class t{}return t.\u0275mod=u.Kb({type:t}),t.\u0275inj=u.Jb({factory:function(e){return new(e||t)},providers:[d.a,p.a,r.a,b.a],imports:[[n.c,x.a,c.f,o.i,o.u]]}),t})();var T=i("/4iV");i.d(e,"DashboardModule",(function(){return B}));const L=[{path:"",data:{title:"Account",topmenuIndex:T.c.TOPMENU_SETTINGS_INDEX,leftmenuIndex:T.c.SETTINGS_OVERVIEW_INDEX,urls:[{title:"Account Overview"}]},component:$}];let B=(()=>{class t{}return t.\u0275mod=u.Kb({type:t}),t.\u0275inj=u.Jb({factory:function(e){return new(e||t)},imports:[[o.i,n.c,x.a,A,c.f.forChild(L)]]}),t})()}}]);