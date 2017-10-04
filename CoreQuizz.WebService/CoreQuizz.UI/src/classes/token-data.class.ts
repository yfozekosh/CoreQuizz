export class TokenData {
  createdAt: number;

  constructor(public token: string, public expiration: number) {
    this.createdAt = new Date().getTime() / 1000;
  }
}
