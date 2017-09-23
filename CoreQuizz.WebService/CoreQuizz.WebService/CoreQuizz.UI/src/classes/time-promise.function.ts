export const Timeout = (miliseconds: number): Promise<any> => {
  return new Promise((res) => {
    setTimeout(() => {
      res();
    }, miliseconds);
  });
};
