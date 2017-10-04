import {Directive, ViewContainerRef} from '@angular/core';

@Directive({
  selector: '[appDefinitionHost]',
})
export class DefinitionHostDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}
