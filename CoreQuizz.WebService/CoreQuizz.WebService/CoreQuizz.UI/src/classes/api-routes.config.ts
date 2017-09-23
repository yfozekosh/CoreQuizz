import {environment} from '../environments/environment';

const urljoin = (...args: string[]) => {
  let result = '';
  for (const arg of args) {
    if (!result) {
      result = arg;
    } else {
      if (!result.endsWith('/')) {
        if (!arg.startsWith('/')) {
          result = result + '/';
        }
        result = result + arg;
      } else {
        if (arg.startsWith('/')) {
          result = result + arg.slice(1);
        } else {
          result = result + arg;
        }
      }
    }
  }
  return result;
};

const _api_url = environment.apiUrl;

const _account = urljoin(_api_url, 'account');
const _survey = urljoin(_api_url, 'survey');

export const ApiRoutes = {
    account: {
      register: urljoin(_account, 'register')
    },
    token: urljoin(_api_url, 'token'),
    survey: {
      getAll: urljoin(_survey, 'get-all'),
      getGlobal: urljoin(_survey, 'get-global'),
      get: (id: number) => urljoin(_survey, id.toString()),
      edit: (id: number) => urljoin(_survey, id.toString(), 'edit'),
      create: urljoin(_survey, 'create'),
      save: urljoin(_survey, 'save'),
    }
  }
;
