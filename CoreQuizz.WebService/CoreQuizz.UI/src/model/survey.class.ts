import {QuestionDefinition} from './question-definition.abstract';

export class Survey {
  surveyId: number;
  surveyName: string;
  createdDate: Date;
  description: string;
  modifiedDate: Date;
  stars: number;
  access: number;
  createdBy: string;
}

export class SurveyWithDefinition extends Survey {
  questionDefinitions: QuestionDefinition[];
}
