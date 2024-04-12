import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HangfireService } from './hangfire.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  hangfireStates: any[] = [];

  constructor(private http: HttpClient, private hangfireService: HangfireService) {}

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
