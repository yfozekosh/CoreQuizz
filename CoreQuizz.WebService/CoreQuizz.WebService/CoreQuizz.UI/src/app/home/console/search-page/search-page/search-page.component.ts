import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {SurveyService} from '../../../../../services/survey.service';
import {Observable} from 'rxjs/Observable';
import {Survey} from '../../../../../model/survey.class';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.scss']
})
export class SearchPageComponent implements OnInit {
  searchText: string;
  surveys: Observable<Survey[]>;

  constructor(private _activeRoute: ActivatedRoute, private _surveyService: SurveyService) {

  }

  ngOnInit() {
    this._activeRoute.queryParams.subscribe((params: Params) => {
      this.searchText = params['search-text'];
      this.surveys = this._surveyService.getGlobalSurveys(this.searchText, 50, 0).map(s => s.value);
    });
  }
}
