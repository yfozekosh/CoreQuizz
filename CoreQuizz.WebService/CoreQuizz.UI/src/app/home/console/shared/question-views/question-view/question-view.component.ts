import {
  Component, ComponentFactoryResolver, EventEmitter, Inject, Input, OnChanges, OnInit,
  Output, ViewChild
} from '@angular/core';
import {QuestionView} from '../question-view.abstract-component';
import {QuestionDefinition} from '../../../../../../model/question-definition.abstract';
import {DefinitionHostDirective} from '../../../survey-edit-page/components/survey-question-definition/definition-host.directive';
import * as QuestionDefinitions from '../../../../../../model/question-definition.class';

@Component({
  selector: 'app-question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.scss']
})
export class QuestionViewComponent implements OnChanges {
  views: any[];
  @Input() questionDefinition: QuestionDefinition;

  @ViewChild(DefinitionHostDirective) definitionHost: DefinitionHostDirective;

  constructor(private componentFactoryResolver: ComponentFactoryResolver,
              @Inject('QuestionViews') definitions) {
    this.views = definitions;
  }

  ngOnChanges() {
    const regex = /^[A-Z, a-z][a-z]+/g;
    let questionViewComponent;

    for (const key of Object.keys(QuestionDefinitions)) {
      if (this.questionDefinition.type === new QuestionDefinitions[key]().type) {
        const questionType = regex.exec(key)[0];
        questionViewComponent = this.views.find(d => d.name.startsWith(questionType));
      }
    }

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(questionViewComponent);

    const component = this.definitionHost.viewContainerRef.createComponent(componentFactory);
    const instance: QuestionView = (<QuestionView>component.instance);
    instance.question = this.questionDefinition;
  }
}
