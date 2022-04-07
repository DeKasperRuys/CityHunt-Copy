import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { SignUpComponent } from "./sign-up/sign-up.component";
import { QuestionsComponent } from './questions/questions.component';
import { TeamComponent } from './team/team.component';
import { ResetPasswordComponent } from "./reset-password/reset-password.component";
import { AuthGuard } from "./Services/AuthGuard";
import { AbilityComponent } from './ability/ability.component';

export const appRoutes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'login', component: LoginComponent },
  { path: 'SignUp', component: SignUpComponent },
  { path: 'questions', component: QuestionsComponent, canActivate: [AuthGuard]},
  { path: 'team', component: TeamComponent, canActivate: [AuthGuard] },
  { path: 'reset', component: ResetPasswordComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'ability', component: AbilityComponent, canActivate: [AuthGuard] },

]
