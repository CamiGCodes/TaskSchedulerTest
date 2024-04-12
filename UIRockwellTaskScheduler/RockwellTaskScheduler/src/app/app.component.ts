import { Component } from '@angular/core';
import {HttpClient } from '@angular/common/http'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private http: HttpClient){

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
}
