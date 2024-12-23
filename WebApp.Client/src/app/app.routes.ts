import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { MembersListComponent } from './admin/members-list/members-list.component';
import { MemberDetailComponent } from './admin/member-detail/member-detail.component';
import { MemberCreateComponent } from './admin/member-create/member-create.component';
import { ReportComponent } from './admin/report/report.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';
import { TrainersComponent } from './trainers/trainers.component';
import { SupplementComponent } from './supplement/supplement.component';
import { AboutusComponent } from './admin/aboutus/aboutus.component';
import { PlanListComponent } from './admin/plan-list/plan-list.component';
import { adminAuthGuard } from './_guards/admin-auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { MemberEditComponent } from './admin/member-edit/member-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MembershipCreateComponent } from './admin/membership-create/membership-create.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path:'',
        runGuardsAndResolvers:'always',
        canActivate:[adminAuthGuard],
        children:[
            {path: 'admin-dashboard', component: DashboardComponent},
            {path: 'members', component: MembersListComponent},
            {path: 'member-create', component: MemberCreateComponent},
            {path: 'member-edit/:memberLoginName', component: MemberEditComponent, canDeactivate:[preventUnsavedChangesGuard]},
            {path: 'report', component: ReportComponent},
            {path: 'maintenance', component: MaintenanceComponent},
            {path: 'membership-create/:memberLoginName', component: MembershipCreateComponent},
        ]
    },
    {path: 'member-detail/:memberLoginName', component: MemberDetailComponent},
    {path: 'trainers', component: TrainersComponent},
    {path: 'supplements', component: SupplementComponent},
    {path: 'aboutus', component: AboutusComponent},
    {path: 'plans', component: PlanListComponent},
    {path: 'errors', component: TestErrorsComponent},
    {path: '**', component: HomeComponent, pathMatch:'full'},
];
