import {
  Component,
  ComponentFactoryResolver,
  EventEmitter,
  Inject,
  Input,
  OnChanges,
  OnInit,
  Output,
  ViewChild
} from '@angular/core';
import * as QuestionDefinitions from '../../../../../../model/question-definition.class';
import {QuestionDefinition} from '../../../../../../model/question-definition.abstract';
import {DefinitionComponent} from '../../../shared/definition-views/definition-component';
import {DefinitionHostDirective} from './definition-host.directive';

@Component({
  selector: 'app-survey-question-definition',
  templateUrl: 'survey-question-definition.component.html',
  styleUrls: ['survey-question-definition.component.scss']
})
export class SurveyQuestionDefinitionComponent implements OnInit, OnChanges {
  @Input() questionDefinition: QuestionDefinition;
  @Output() questionDefinitionChange: EventEmitter<QuestionDefinition> = new EventEmitter();

  @ViewChild(DefinitionHostDirective) definitionHost: DefinitionHostDirective;

  private definitions;

  constructor(private componentFactoryResolver: ComponentFactoryResolver,
              @Inject('QuestionDefinitions') definitions) {
    this.definitions = definitions;
  }

  ngOnInit() {
  }

  ngOnChanges() {
    const regex = /^[A-Z, a-z][a-z]+/g;
    let questionDefinitionComponent;

    for (const key of Object.keys(QuestionDefinitions)) {
      if (this.questionDefinition.type === new QuestionDefinitions[key]().type) {
        const questionType = regex.exec(key)[0];
        questionDefinitionComponent = this.definitions.find(d => d.name.startsWith(questionType));
      }
    }

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(questionDefinitionComponent);

    const component = this.definitionHost.viewContainerRef.createComponent(componentFactory);
    const instance: DefinitionComponent = (<DefinitionComponent>component.instance);
    instance.question = this.questionDefinition;
    instance.questionChange.subscribe((newQuestion) => {
      instance.question = newQuestion;
      this.questionDefinition = newQuestion;
      this.questionDefinitionChange.emit(this.questionDefinition);
    });

    const subscriber = instance.onTypeChange.subscribe((newType) => {
      if (newType) {
        const prevDefinition = this.questionDefinition;
        this.questionDefinition = new newType();
        this.questionDefinition.questionLabel = prevDefinition.questionLabel;
        this.questionDefinitionChange.emit(this.questionDefinition);
        subscriber.unsubscribe();
      }
    });
  }
}
