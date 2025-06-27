import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { TodoitemRepository } from './api/TodoitemRepository'
import emitter from './eventBus'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.provide('TodoitemRepository', new TodoitemRepository(emitter));
app.mount('#app')
