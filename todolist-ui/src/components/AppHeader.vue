<template>
  <header class="app-header">
    <div class="app-name-container">
        ToDo List
    </div>
    <div class="user-info">
      Zalogowany jako: <span class="username">{{ username }}</span>
    </div>
    <button class="logout-btn" @click="emit('logout')">Wyloguj</button>
  </header>
</template>
<script setup>
const emit = defineEmits(['logout']);

const props = defineProps({
    username: {
        type: String,
        required: true
    }
});

</script>
<style scoped>
.app-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #1f2937; /* szary-900 */
  color: white;
  padding: 16px 24px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
}

.app-name-container{
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-size: 30px;
    font-weight: bold;
}

.user-info {
  font-size: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.username {
  font-weight: bold;
  color: #3b82f6; /* niebieski-500 */
  margin-left: 5px;
}

.logout-btn {
  background-color: #ef4444; /* czerwony-500 */
  border: none;
  color: white;
  padding: 8px 16px;
  border-radius: 6px;
  font-size: 14px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.logout-btn:hover {
  background-color: #dc2626; /* czerwony-600 */
}

</style>