<template>
   <transition name="fade-slide" mode="out-in">
  <div :key="isRegister" class="container">
    <div class="login-container">
      <div class="form-header">{{isRegister ? "Rejestracja" : "Logowanie"}}</div>
      <form class="form" @submit.prevent="submit(form)">
        <div>
          <label for="email">Login</label>
          <input v-model="form.username" type="username" id="username" />
        </div>
        <div>
          <label for="password">Hasło</label>
          <input v-model="form.password" type="password" id="password" label="Hasło" />
        </div>
        <button class="submit-button" type="submit">{{isRegister ? "Zarejestruj się" : "Zaloguj się"}}</button>
      </form>
        <button class="switch-button" @click="isRegister=!isRegister">{{isRegister ? "Logowanie" : "Utwórz konto" }}</button>
    </div>
  </div>
  </transition>
</template>
<script setup>
import { ref } from 'vue';
import { useAuthStore } from '@/stores/auth';
import { useRouter } from 'vue-router';
const form = ref({username: '', password: ''});
const auth = useAuthStore();
const router = useRouter();
const isRegister = ref(false);

const submit = () => {
  if(isRegister.value){
    auth.register(form)
  }else{
    auth.login(form, router)
  }
};

</script>
<style scoped>
.container {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.login-container {
  background-color: #ffffff;
  padding: 40px 30px;
  border-radius: 12px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
}

.form-header {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  font-size: 24px;
  font-weight: bold;
  text-align: center;
  color: #333;
  margin-bottom: 20px;
}

.form {
  display: grid;
  gap: 16px;
}

label {
  display: block;
  font-size: 20px;
  color: #555;
  margin-bottom: 6px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

input[type="username"],
input[type="password"] {
  width: 93%;
  padding: 10px 12px;
  font-size: 15px;
  border: 1px solid #aaa;
  border-radius: 8px;
  transition: 0.2s;
}

input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.15);
}

.submit-button {
  background-color: #007bff;
  color: #fff;
  border: none;
  padding: 12px;
  font-size: 16px;
  margin-top: 20px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.submit-button:hover {
  background-color: #0056b3;
}

.switch-button{
  background-color: #4f46e5; /* indygo-600 */
  color: white;
  padding: 10px 20px;
  margin-top: 20px;
  font-size: 1rem;
  font-weight: 500;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  width: 100%;
  transition: background-color 0.3s ease, transform 0.2s ease;
}
.switch-button:hover{
  background-color: #4338ca; /* indygo-700 */
}

.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: all 0.4s ease;
}

.fade-slide-enter-from {
  opacity: 0;
  transform: translateY(20px);
}
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}

</style>