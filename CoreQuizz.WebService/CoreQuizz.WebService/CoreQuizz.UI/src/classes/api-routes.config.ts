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

const _api_url = '/api';
const _account = urljoin(_api_url, 'account');

export const ApiRoutes = {
  account: {
    register: urljoin(_account, 'register')
  },
  token: urljoin(_api_url, 'token')
};
