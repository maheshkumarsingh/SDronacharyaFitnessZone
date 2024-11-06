import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const adminAuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if(accountService.currentMember()?.memberLoginName === 'meet2mahesh17')
  {
    return true;
  }else{
    toastr.error('Only admin Allowed');
    return false;
  }
};
