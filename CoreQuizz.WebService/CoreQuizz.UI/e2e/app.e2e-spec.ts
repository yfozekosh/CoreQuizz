import { CoreQuizzUIPage } from './app.po';

describe('core-quizz-ui App', () => {
  let page: CoreQuizzUIPage;

  beforeEach(() => {
    page = new CoreQuizzUIPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
