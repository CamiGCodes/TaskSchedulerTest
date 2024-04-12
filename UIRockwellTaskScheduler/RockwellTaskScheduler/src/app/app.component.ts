import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HangfireService } from './hangfire.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; // Importar FormBuilder y Validators
import { cronExpressionValidator } from './validators'; // Importar el validador de expresión Cron

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  taskSchedulerForm!: FormGroup; // Indicar que la variable puede ser nula

  hangfireStates: any[] = [];

  constructor(private http: HttpClient, private hangfireService: HangfireService, private formBuilder: FormBuilder) {
    this.taskSchedulerForm = this.formBuilder.group({
      cronExpression: ['', [Validators.required, cronExpressionValidator()]], // Validación personalizada para la expresión Cron
      url: ['', [Validators.required, Validators.pattern('https?://.+')]], // Validación de URL utilizando Validators.pattern
    });
  }
  ngOnInit(): void {
    // Inicializar el formulario con las validaciones
    this.taskSchedulerForm = this.formBuilder.group({
      cronExpression: ['', [Validators.required, cronExpressionValidator()]], // Validación personalizada para la expresión Cron
      url: ['', [Validators.required, Validators.pattern('https?://.+')]], // Validación de URL utilizando Validators.pattern
    });
  }

  onSubmit(data: any) {
    const transformedData = {
      cron: {
        minutes: data.cronExpression.split(' ')[0],
        hours: data.cronExpression.split(' ')[1],
        dayOfMonth: data.cronExpression.split(' ')[2],
        month: data.cronExpression.split(' ')[3],
        dayOfWeek: data.cronExpression.split(' ')[4],
      },
      url: data.url,
    };
  
    this.http.post('https://localhost:44390/api/tasks/scheduleTask', transformedData)
      .subscribe((result) => {
        console.warn('result', result);
      });
  }

  loadHangfireStates() {
    this.hangfireService.getHangfireStates().subscribe((states: any[]) => {
      this.hangfireStates = states;
    });
  }
}
