"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.SharedModule = void 0;
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var user_roles_component_1 = require("./user-roles/user-roles.component");
var chips_1 = require("@angular/material/chips");
var display_image_component_1 = require("./display-image/display-image.component");
var mat_file_upload_1 = require("mat-file-upload");
var display_image_directive_1 = require("./display-image.directive");
var SharedModule = /** @class */ (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        core_1.NgModule({
            declarations: [user_roles_component_1.UserRolesComponent, display_image_component_1.DisplayImageComponent, display_image_directive_1.DisplayImageDirective],
            imports: [
                common_1.CommonModule,
                chips_1.MatChipsModule,
                mat_file_upload_1.MatFileUploadModule
            ], exports: [user_roles_component_1.UserRolesComponent, display_image_component_1.DisplayImageComponent]
        })
    ], SharedModule);
    return SharedModule;
}());
exports.SharedModule = SharedModule;
