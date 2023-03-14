import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PreventUnsavedFormChangesGuard } from '../core/guards/prevent-unsaved-changes/prevent-unsaved-form-changes.guard';
import { CurrentMemberEditComponent } from './components/edit/current-member-edit.component';

const routes: Routes = [
    {
        path: '',
        component: CurrentMemberEditComponent,
        canDeactivate: [PreventUnsavedFormChangesGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: [PreventUnsavedFormChangesGuard]
})
export class CurrentMemberRoutingModule { }