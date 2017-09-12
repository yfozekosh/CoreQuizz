export class OptionsDefinition {
  value: string;
  isSelected: boolean;

  constructor(value?: string, isSelected?: boolean) {
    this.value = value;
    this.isSelected = !!isSelected;
  }
}
