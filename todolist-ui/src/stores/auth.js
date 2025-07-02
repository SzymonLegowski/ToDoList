import { defineStore } from "pinia";
import axios from '@/axios';
import { ref, computed } from 'vue';
import emitter from '@/eventBus';

export const useAuthStore = defineStore('auth', () =>{
    const accessToken = ref(localStorage.getItem('accessToken') || '');
    const refreshToken = ref(localStorage.getItem('refreshToken') || '');
    const user = ref(localStorage.getItem('username') || null);
    
    
    function saveTokens(access, refresh){
        accessToken.value = access;
        refreshToken.value = refresh;
        localStorage.setItem('accessToken', access);
        localStorage.setItem('refreshToken', refresh);
    }

    async function login(form, router) {
        try{
            const res = await axios.post('/auth/login', form.value);
            saveTokens(res.data.accessToken, res.data.refreshToken);
            user.value = {...form.value, userId: res.data.userId};
            localStorage.setItem('userId', user.value.userId);
            localStorage.setItem('username', user.value.username);
            router.push({name: 'dashboard'})
        }
        catch(err){
            console.log(err);
            console.log(err.response.data);
            emitter.emit('alert', {
                msg: err.response.data,
                variant: 'error'
            })
        }
    }

    async function logout(router){
        try{
            await axios.post('/Auth/logout');
            localStorage.removeItem('accessToken');
            localStorage.removeItem('refreshToken');
            localStorage.removeItem('userId');
            user.value = null;
            accessToken.value = '';
            refreshToken.value = '';
            user.value = '';
            router.push({name: 'auth'});
        }catch(err){
            const message = err.response?.data || err.message || 'Nieznany błąd';
            console.log(err);
            emitter.emit('alert', {
                msg: message,
                variant: 'error'
            })
        }
    }

    async function register(form){
        try{
            const res = await axios.post('/Auth/register', form.value);
            console.log(res.data);
        }catch (err){
            console.log(err.response.data);
              emitter.emit('alert', {
                msg: err.response.data,
                variant: 'error'
            })
        }
    }

    const isLogged = computed(() => {
        return !!accessToken.value;
    });

    return { accessToken, refreshToken, user, register, login, logout, saveTokens, isLogged };
});