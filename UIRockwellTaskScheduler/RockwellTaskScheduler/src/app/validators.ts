import { AbstractControl, ValidatorFn } from '@angular/forms';

// Expresión regular para validar la expresión Cron
const cronExpressionPattern = /^(\*|[0-5]?\d) (\*|[01]?\d|2[0-3]) (\*|[1-9]|[12]\d|3[01]) (\*|[1-9]|1[0-2]) (\*|[0-6])$/;

export function cronExpressionValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const cronExpression = control.value;
    // Verificar si la expresión Cron cumple con el patrón de la expresión regular
    const isValid = cronExpressionPattern.test(cronExpression);

    return isValid ? null : { 'invalidCronExpression': { value: cronExpression } };
  };
}
