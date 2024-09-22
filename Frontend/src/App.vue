<template>
  <header>
    <v-toolbar title="ShoppingApp">
      <v-btn class="cart-badge"
          @click="router.push({ name: 'CartView' })"
      >{{ store.cart.length }}
      </v-btn>
    </v-toolbar>
  </header>
  <main>
    <RouterView />
  </main>
</template>

<script setup>
  import { useRouter } from "vue-router";
  import { productsStore } from "@/stores/products";

  const router = useRouter()
  const store = productsStore()
</script>


<style scoped>
.cart-items {
  text-align: end;
  padding: 16px;
  font-weight: bold;
  font-size: 24px;
  cursor: pointer;
}
.cart-badge {
  background-color: #F178B6;
  color: white;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  min-width: 0;
}
</style>
