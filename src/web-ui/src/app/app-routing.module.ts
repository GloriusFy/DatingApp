import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SignupComponent } from './auth/signup/signup.component';
import { AuthGuard } from './core/guards/auth/auth.guard';
import { PreventUnsavedFormChangesGuard } from './core/guards/prevent-unsaved-changes/prevent-unsaved-form-changes.guard';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';

const routes: Routes = [
    {
        path: "",
        component: HomeComponent,
        pathMatch: "full"
    },
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "signup",
        component: SignupComponent
    },
    {
        path: "",
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {
                path: "members",
                loadChildren: () => import('./members/members.module').then(m => m.MembersModule),
            },
            {
                path: "member/edit",
                loadChildren: () => import('./current-member/current-member.module').then(m => m.CurrentMemberModule)
            },
            {
                path: "matches",
                loadChildren: () => import('./matches/matches.module').then(m => m.MatchesModule)
            },
            {
                path: "messages",
                component: MessagesComponent
            },
        ]
    },
    {
        path: "not-found",
        component: NotFoundComponent
    },
    {
        path: "**",
        component: NotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [PreventUnsavedFormChangesGuard]
})
export class AppRoutingModule { }
