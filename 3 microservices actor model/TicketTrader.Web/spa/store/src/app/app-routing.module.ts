import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SeatingPlanMainViewComponent } from './seating-plan//components/seating-plan-main-view/seating-plan-main-view.component';
import { BasketMainViewComponent } from './basket/basket-main-view/basket-main-view.component';

import { PageNotFoundComponent } from './access-control/page-not-found.component';
import { ForbiddenComponent } from 'app/access-control/forbidden.component';
import { AuthorizeComponent } from 'app/access-control/authorize.component';
import { AuthGuard } from 'app/services/auth.guard';
import { EventGuard } from 'app/services/event.guard';
import { BannedGuard } from 'app/services/banned.guard';
import { PostAuthorizeRedirectComponent } from 'app/access-control/post-authorize-redirect.component';
import { SummaryComponent } from 'app/basket/summary/summary.component';


const appRoutes: Routes = [
    { path: 'Authorize', component: AuthorizeComponent },
    { path: 'AuthorizeRedirect', component: PostAuthorizeRedirectComponent },
    { path: 'SeatingPlan/:eventId', component: SeatingPlanMainViewComponent, canActivate: [AuthGuard, BannedGuard] },
    { path: 'Basket', component: BasketMainViewComponent, canActivate: [AuthGuard, BannedGuard] },
    { path: 'Summary', component: SummaryComponent, canActivate: [AuthGuard, BannedGuard] },
    { path: 'Forbidden', component: ForbiddenComponent },
    { path: '**', component: PageNotFoundComponent },
    { path: 'NotFound', component: PageNotFoundComponent },
    { path: 'Summary', component: SummaryComponent }
];


@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
