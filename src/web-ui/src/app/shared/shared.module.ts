import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ImageWithFallbackDirective } from './directives/image-fallback.directive';
import { OnlineStatusIndicatorComponent } from './components/online-status-indicator/online-status-indicator.component';

@NgModule({
    declarations: [
        NavBarComponent,
        NotFoundComponent,
        ImageWithFallbackDirective,
        OnlineStatusIndicatorComponent
    ],
    exports: [
        NavBarComponent,
        ImageWithFallbackDirective,
        OnlineStatusIndicatorComponent
    ],
    imports: [
        CommonModule,
        MatToolbarModule,
        MatListModule,
        MatButtonModule,
        MatMenuModule,
        MatIconModule,
        RouterModule
    ]
})
export class SharedModule { }
