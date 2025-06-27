import { createRouter, createWebHistory } from 'vue-router'
import AuthView from '@/views/AuthView.vue'
import Dashboard from '../views/Dashboard.vue'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'auth', component: AuthView, meta: { guestOnly: true} },
    { path: '/dashboard', name: 'dashboard', component: Dashboard, meta:{ requiresAuth: true } },
    { path: '/:pathMatch(.*)*', redirect: '/'},
  ],
})

router.beforeEach((to, from, next) => {
    const auth = useAuthStore();
    if (to.matched.some(record => record.meta.requiresAuth)){
        if (!auth.isLogged) {
            next({
                path: '/',
                query: { redirect: to.fullPath }
            });
        } else {
            next();
        } 
    } else if (to.matched.some(record => record.meta.guestOnly)){
        if (auth.isLogged) {
            next({
                path: '/dashboard',
                query: { redirect: to.fullPath }
            });
        } else {
            next();
        }
    }
     else {
        next();
    }
});

export default router
