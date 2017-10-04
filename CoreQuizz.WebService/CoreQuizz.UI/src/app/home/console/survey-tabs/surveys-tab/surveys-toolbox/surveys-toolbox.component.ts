import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {AccessTypes} from '../../../../../../model/survey-access-type';

@Component({
  selector: 'app-surveys-toolbox',
  templateUrl: 'surveys-toolbox.component.html',
  styleUrls: ['surveys-toolbox.component.scss']
})
export class SurveysToolboxComponent implements OnInit {
  @Input() searchBox: string;
  @Output() searchBoxChange: EventEmitter<string> = new EventEmitter<string>();

  accessTypes = [{code: 0, display: 'All'}, ...AccessTypes];

  selectedType = this.accessTypes[0].code;

  searchText = this.fb.control('');
  accesType = this.fb.control(0);

  constructor(private fb: FormBuilder) {
  }

  ngOnInit(): void {
    console.log(this.searchBox);
    this.searchText.setValue(this.searchBox);
  }

  handleChange() {
    this.searchBox = this.searchText.value;
    this.searchBoxChange.emit(this.searchText.value);
  }
}
