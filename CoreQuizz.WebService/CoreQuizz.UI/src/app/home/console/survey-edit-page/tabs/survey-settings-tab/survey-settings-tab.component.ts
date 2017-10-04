import {Component, EventEmitter, Input, Output, Inject} from '@angular/core';
import {AccessTypes} from '../../../../../../model/survey-access-type';
import {Survey} from '../../../../../../model/survey.class';
import {MdDialogRef, MD_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-survey-settings-tab',
  templateUrl: 'survey-settings-tab.component.html',
  styleUrls: ['survey-settings-tab.component.scss']
})
export class SurveySettingsTabComponent {
  @Input() survey: Survey;
  @Output() onAccessChange = new EventEmitter<number>();
  @Output() onDelete = new EventEmitter();

  accessTypes = AccessTypes;

  private _selectedAccess: number = this.accessTypes[0].code;

  get selectedAccess(): number {
    return this._selectedAccess;
  }

  set selectedAccess(value: number) {
    this._selectedAccess = value;
    this.onAccessChange.next(this._selectedAccess);
  }

  handleDelete() {

  }
}

@Component({
  selector: 'app-delete-dialog',
  template: `
  `
})
export class DeleteDialogComponent {
  constructor(public dialogRef: MdDialogRef<SurveySettingsTabComponent>,
              @Inject(MD_DIALOG_DATA) public data: any) {
  }
}
