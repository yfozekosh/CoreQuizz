import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-edit-block-container',
  templateUrl: 'edit-block-container.component.html',
  styleUrls: ['edit-block-container.component.scss']
})
export class EditBlockContainerComponent {
  @Input() isActive = false;
  @Input() isCanDelete = false;
  @Input() isCanClone = false;
  @Input() isCanMove = false;

  @Output() onClick = new EventEmitter();
  @Output() onUp = new EventEmitter();
  @Output() onDown = new EventEmitter();
  @Output() onClone = new EventEmitter();
  @Output() onDelete = new EventEmitter();

  get isCanSomething() {
    return this.isCanDelete || this.isCanClone || this.isCanMove;
  }
}
