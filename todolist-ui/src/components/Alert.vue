<template>
  <transition name="fade">
    <div v-if="visible" :class="['alert', alertType]">
      {{ message }}
    </div>
  </transition>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import emitter from '@/eventBus' // patrz niżej

const visible = ref(false)
const message = ref('')
const type = ref('info') // 'info' lub 'error'

const alertType = computed(() => {
  return type.value === 'error' ? 'alert-error' : 'alert-info'
})

const showAlert = ({ msg, variant }) => {
  message.value = msg
  type.value = variant || 'info'
  visible.value = true
  setTimeout(() => {
    visible.value = false
  }, 4000)
}

onMounted(() => {
  emitter.on('alert', showAlert)
})
</script>

<style scoped>
.alert {
  position: fixed;
  top: 20px;
  left: 50%;
  transform: translateX(-50%);
  padding: 14px 24px;
  border-radius: 8px;
  font-weight: 500;
  font-size: 32px;
  color: white;
  z-index: 1000;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.alert-info {
  background-color: #3b82f6; /* blue-500 */
}

.alert-error {
  background-color: #ef4444; /* red-500 */
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
