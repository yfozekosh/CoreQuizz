export abstract class ServiceResponse<TValueType> {
  value: TValueType;
  error: string;

  constructor(public isSuccess: boolean, value: TValueType | string) {
    if (isSuccess) {
      this.value = <TValueType>value;
      this.error = null;
    } else {
      this.value = null;
      this.error = <string>value;
    }
  }
}

export class OkServiceResponse<TValueType> extends ServiceResponse<TValueType> {
  constructor(value: TValueType) {
    super(true, value);
  }
}

export class ErrorServiceResponse extends ServiceResponse<any> {
  constructor(error: string) {
    super(false, error);
  }
};
