import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {SurveyService} from '../../../../services/survey.service';
import {Router} from '@angular/router';
@Component({
  selector: 'app-new-survey-page',
  templateUrl: 'new-survey.component.html',
  styleUrls: ['new-survey.component.scss']
})
export class NewSurveyComponent {
  form: FormGroup;
  err: string;

  constructor(private fb: FormBuilder, private _surveyService: SurveyService, private _router: Router) {
    this.form = fb.group({
      title: fb.control('', [
        Validators.required,
        Validators.maxLength(255)
      ]),
      description: fb.control('', [
        Validators.maxLength(1024)
      ]),
      access: fb.control(1)
    });
  }

  handleCreate() {
    const value: {
      title: string,
      description: string,
      access: number
    } = this.form.value;

    this._surveyService.createSurvey(value.title, value.access, value.description).subscribe(d => {
      if (d.isSuccess) {
        this._router.navigateByUrl('/console');
      } else {
        this.err = d.error;
      }
    });
    console.log(this.form.value);
  }
}
