import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-edit-block-container',
  templateUrl: 'edit-block-container.component.html',
  styleUrls: ['edit-block-container.component.scss']
})
export class EditBlockContainerComponent {
  @Input() isActive = false;
  @Output() onClick =  new EventEmitter();
}
