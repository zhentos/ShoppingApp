<template>
  <v-btn color="primary"
         variant="elevated" 
         @click="router.push({ name: 'Catalog' })">Back to catalog</v-btn>

  <div v-if="!store.cart.length" style="text-align: center">
    <h1>Empty Cart ...</h1>
  </div>
  <div class="cart-items" v-else>
    <div
      class="cart-item"
      v-for="item in store.cart"
      :key="item.id"
    >
      <div class="item-details">
        <img :src="item.thumbnail" alt="" class="item-image">
        <div class="item-info">
          <div class="first-line">
            <span class="category">{{ item.category }}</span>
            <span class="price">${{ item.price }}</span>
          </div>
          <div class="second-line">
            <span class="title">{{ item.title }}</span>
            <button @click="removeFromCart(item.id)" class="remove-button">Remove</button>
          </div>
          <div class="counter">
            <button @click="decrementCounter(item.id)" class="counter-button">-</button>
            <span class="counter-value">{{ getCounterValue(item.id) }}</span> 
            <button @click="incrementCounter(item.id)" class="counter-button">+</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import "@/assets/styles.scss";

import { defineComponent } from "vue";
export default defineComponent({
  name: 'CartView'
})
</script>

<script setup>
import { productsStore } from "@/stores/products";
import { useRouter } from "vue-router";
import { ref } from "vue";

const router = useRouter();
const store = productsStore();

// Create a reactive object to hold counter values
const counters = ref({});

const getCounterValue = (id) => {
  return counters.value[id] || 1; // Default to 1 if not set
};

const incrementCounter = (id) => {
  counters.value[id] = (counters.value[id] || 1) + 1; // Increment by 1
};

const decrementCounter = (id) => {
  if ((counters.value[id] || 1) > 1) {
    counters.value[id] -= 1; // Decrement by 1 if greater than 1
  }
};

// Remove item from cart
const removeFromCart = (id) => {
  store.removeFromCart(id);
}
</script>

<style scoped>

.cart-items {
  margin-top: 10px;
}

.item-details {
  display: flex;
  align-items: flex-start; /* Align items at the start */
  margin-bottom: 32px;
  box-shadow: 0 0 17px 6px #e7e7e7;
  border-radius: 8px;
  padding: 16px;
}

.item-image {
  width: 20%; /* Set width for image */
}

.item-info {
  flex-grow: 1; /* Allow item info to take remaining space */
  margin-left: 16px; /* Add space between image and text */
}

.first-line {
  display: flex;
  justify-content: space-between; /* Space between category and price */
  margin-bottom: 8px; /* Add gap below the second line */
}

.category,
.price {
  font-weight: bold; /* Make category and price bold */
}

.counter {
  display: flex; /* Use flexbox for counter layout */
  align-items: center; /* Center items vertically */
  border: 1px solid #c0c0c0; /* Gray border */
  padding: 4px; /* Optional padding for aesthetics */
  border-radius: 6px; /* Round the edges */
  max-width: 60px; /* Limit the maximum width to 25px */
}

.counter-button {
  background-color: transparent; /* Make button background transparent */
  border: none; /* Remove border */
  font-size: 16px; /* Set a specific font size */
  cursor: pointer; /* Change cursor to pointer on hover */
  margin: 0 7px; /* Add horizontal margin to buttons */
}

.counter-value {
  margin: 0; /* Remove margin around counter value for better centering */
  font-size: 16px; /* Match font size with buttons */
}

.second-line {
  display: flex;
  justify-content: space-between; /* Space between title and remove button */
  margin-bottom: 8px; /* Add gap below the second line */
}

</style>