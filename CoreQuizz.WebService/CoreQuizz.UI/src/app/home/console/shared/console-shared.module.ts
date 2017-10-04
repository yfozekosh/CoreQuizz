import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {InputQuestionViewComponent} from './question-views/input-question-view/input-question-view.component';
import {CheckboxQuestionViewComponent} from './question-views/checkbox-question-view/checkbox-question-view.component';
import {RadioQuestionViewComponent} from './question-views/radio-question-view/radio-question-view.component';
import {DefinitionViewsModule} from './definition-views/definition-views.module';
import {QuestionViewComponent} from './question-views/question-view/question-view.component';
import {DefinitionHostDirective} from '../survey-edit-page/components/survey-question-definition/definition-host.directive';

export const QuestionViews = [
  InputQuestionViewComponent,
  CheckboxQuestionViewComponent,
  RadioQuestionViewComponent
];

@NgModule({
  imports: [
    CommonModule,
    DefinitionViewsModule
  ],
  declarations: [
    ...QuestionViews,
    QuestionViewComponent,
    DefinitionHostDirective,
  ],
  exports: [
    ...QuestionViews,

    DefinitionViewsModule,
    QuestionViewComponent,
    DefinitionHostDirective
  ],
  entryComponents: QuestionViews,
  providers: [
    {provide: 'QuestionViews', useValue: QuestionViews}
  ]
})
export class ConsoleSharedModule {
}
