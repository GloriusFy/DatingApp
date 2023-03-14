import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MemberDetailsComponent } from './member-details/member-details.component';
import { MemberListComponent } from './member-list/components/list/member-list.component';
import { MemberResolver } from './services/member.resolver';

const routes: Routes = [
    {
        path: '',
        component: MemberListComponent,
    },
    {
        path: ":id",
        component: MemberDetailsComponent,
        resolve: {
            member: MemberResolver
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: [MemberResolver]
})
export class MembersRoutingModule { }