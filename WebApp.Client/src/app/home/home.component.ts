import { Component} from '@angular/core';
import { PlanListComponent } from "../admin/plan-list/plan-list.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [PlanListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
}
