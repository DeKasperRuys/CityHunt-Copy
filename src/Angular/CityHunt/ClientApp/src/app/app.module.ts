import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { SocialLoginModule, AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from 'angular-6-social-login';
import { AgmCoreModule } from '@agm/core';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { QuestionsComponent } from './questions/questions.component';
import { appRoutes } from './routes';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { from } from 'rxjs';
import { TeamComponent } from './team/team.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AlertModule } from 'ngx-alerts';
import { AbilityComponent } from './ability/ability.component';
import { SearchfiltPipe } from './searchfilt.pipe';

const google_oauth_client_id: string = "76435530142-0m6g262hs6bq5q2aj0t05g4ep55cgoa2.apps.googleusercontent.com";
const facebook_oauth_client_id: string = "410201117041376";

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider(google_oauth_client_id)
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider(facebook_oauth_client_id)
  }

])

export function provideConfig() {
  return config;
}


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    SignUpComponent,
    QuestionsComponent,
    ResetPasswordComponent,
    TeamComponent,
    AbilityComponent,
    SearchfiltPipe
  ],
  imports: [
    HttpClientModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDT8xtgn2M4JZFN30IRKfDj8wQGg9gNl3o'
    }),
    FormsModule,
    BrowserAnimationsModule,
    AlertModule.forRoot(),
    SocialLoginModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [HttpClientModule,{
    provide: AuthServiceConfig, useFactory: provideConfig
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
